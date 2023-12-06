# Exemplo de geração de link de pagamento offline para a Hub4All

## Pré-requisitos
 - Ter uma chave privada fornecida pela Hub4All

## Como utilizar


Modificar o arquivo index.js informando o caminho para a chave privada e os dados do pagamento.
```csharp
...

string chavePrivadaPEM = System.IO.File.ReadAllText("../private.key");

var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
    new Claim("subOrganizationUUID", "8dbcd7bf-68fd-40dd-8b9c-48e02e9d98e8"),
    new Claim("accountUUID", "fd0dd460-8fe1-415f-98be-aa1ca8962ab4"),
    new Claim("paymentProducts", ["mbway", "applepay"]),
    new Claim("uniqueIdentifier", "123456"),
    new Claim("description", "Pagamento de um exemplo da demo de 20-11 para PHC"),
    new Claim("currency", "EUR"),
    new Claim("amount", "25080", ClaimValueTypes.Integer32)
};

...


```

Executar
```bash
dotnet run
```