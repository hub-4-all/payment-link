import { readFileSync } from "fs";
import jwt from "jsonwebtoken";
import qrcode from "qrcode-terminal";

const payload = {
    "subOrganizationUUID": "8dbcd7bf-68fd-40dd-8b9c-48e02e9d98e8",
    "accountUUID": "fd0dd460-8fe1-415f-98be-aa1ca8962ab4",
    "uniqueIdentifier": "123456",
    "paymentProducts": ["mbway", "applepay"],
    "description": "Pagamento de um exemplo da demo de 20-11",
    "currency": "EUR",
    "amount": 11137 // € 100,37
};

const privateKEY  = readFileSync('../private.key', 'utf8');

const signOptions = {
    algorithm:  "RS256"
};

const token = jwt.sign(payload, privateKEY, signOptions);
const url = "https://pay.hub4all.io/" + token;
console.log("Token:  " + token)
console.log("Payment Link:  " + url)
qrcode.generate(url, {small: true});
