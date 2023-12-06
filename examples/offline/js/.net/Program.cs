using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ZXing;
using ZXing.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main()
    {
        string chavePrivadaPEM = System.IO.File.ReadAllText("../private.key");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim("subOrganizationUUID", "8dbcd7bf-68fd-40dd-8b9c-48e02e9d98e8"),
            new Claim("accountUUID", "fd0dd460-8fe1-415f-98be-aa1ca8962ab4"),
            new Claim("uniqueIdentifier", "123456"),
            new Claim("description", "Pagamento de um exemplo da demo de 20-11"),
            new Claim("currency", "EUR"),
            new Claim("amount", "25080", ClaimValueTypes.Integer32)
        };

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(chavePrivadaPEM);

        var key = new RsaSecurityKey(rsa);
        var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var baseUrl = new Uri("https://pay.hub4all.io/");
        var url = new Uri(baseUrl, jwtToken).ToString();

        Console.WriteLine("Token: " + jwtToken);
        Console.WriteLine("Payment Link: " + url);

        var barcodeWriter = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.QrCode.QrCodeEncodingOptions
            {
                Width = 300,
                Height = 300
            }
        };

        var pixelData = barcodeWriter.Write(url);
        
        using var image = Image.LoadPixelData<Rgba32>(pixelData.Pixels, pixelData.Width, pixelData.Height);
        image.Save("qrcode.png");
        Console.WriteLine("QR Code salvo como: qrcode.png");
    }
}
