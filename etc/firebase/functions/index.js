/**
 * Import function triggers from their respective submodules:
 *
 * const {onCall} = require("firebase-functions/v2/https");
 * const {onDocumentWritten} = require("firebase-functions/v2/firestore");
 *
 * See a full list of supported triggers at https://firebase.google.com/docs/functions
 */

const functions = require('firebase-functions');
const admin = require('firebase-admin');
const { FieldValue } = require('firebase-admin/firestore');
admin.initializeApp();
const firestore = admin.firestore();

const {onRequest} = require("firebase-functions/v2/https");
const logger = require("firebase-functions/logger");

// Create and deploy your first functions
// https://firebase.google.com/docs/functions/get-started

// exports.helloWorld = onRequest((request, response) => {
//   logger.info("Hello logs!", {structuredData: true});
//   response.send("Hello from Firebase!");
// });

exports.JsonImport = onRequest(async (req, res) => {
	if (req.method !== 'POST') {
		res.status(405).send('Method Not Allowed');
		return;
	}

	try {
        console.log(req.body);
        await firestore.collection("json").doc().set(req.body);
		res.status(200).send(`{ "ResultCode" : 0, "Message" : "OK" }`);
	} catch (error) {
		console.error('Error processing webhook:', error);
		res.status(500).send(`{ "ResultCode" : 1, "Message" : "Internal Server Error" }`);
	}
});