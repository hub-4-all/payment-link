import { readFileSync } from "fs";
import jwt from "jsonwebtoken";
import qrcode from "qrcode-terminal";

const payload = {
    "subOrganizationUUID": "236a6054-3c4d-436b-8aa8-467f3b2e7160",
    "accountUUID": "954635a4-C68e-4ea5-B1b9-67c9f50fe67b",
    "uniqueIdentifier": "123456",
    "paymentProducts": [],
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
