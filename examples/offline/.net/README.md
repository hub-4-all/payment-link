# Exemplo de geração de link de pagamento offline para a Hub4All

## Pré-requisitos
 - Ter uma chave privada fornecida pela Hub4All

## Como utilizar


Modificar o arquivo index.js informando o caminho para a chave privada e os dados do pagamento.
```csharp
...

string chavePrivadaPEM = System.IO.File.ReadAllText("../private.pem");
IList<string> products = new List<string>();

var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
    new Claim("subOrganizationUUID", "SUBORG_UUID"),
    new Claim("accountUUID", "ACCOUNT_UUID"),
    new Claim("paymentProducts", JsonConvert.SerializeObject(products), JsonClaimValueTypes.JsonArray),
    new Claim("uniqueIdentifier", "123456"),
    new Claim("description", "Pagamento de um exemplo da demo de 20-11"),
    new Claim("currency", "EUR"),
    new Claim("amount", "25080", ClaimValueTypes.Integer32)
};

...


```

Executar
```bash
dotnet run
```