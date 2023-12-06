# Exemplo de geração de link de pagamento offline para a Hub4All

## Pré-requisitos
 - Ter uma chave privada fornecida pela Hub4All

## Como utilizar

Instalar as dependências
```bash
npm install
```
Modificar o arquivo index.js informando os dados do pagamento e o caminho para a chave privada.
```javascript
...

const payload = {
    "subOrganizationUUID": "your-suborg-uuid",
    "accountUUID": "target-account-uuid",
    "uniqueIdentifier": "123456",
    "paymentProducts": ["mbway", "applepay"],
    "description": "Pagamento de Fatura 123456",
    "currency": "EUR",
    "amount": 10037 // € 100,37
};

const privateKEY  = readFileSync('../private.key', 'utf8');

...


```

Executar
```bash
npm start
```