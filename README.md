# OCR Translator Overlay
## Introduction
A desktop overlay which appears after pressing an input defined by the user. The overlay takes a screenshot of what is currently on the screen, fetches all the text via the Cloud Vision API, translates it and puts textboxes in the coordinates of the original text.
Whereas it is being designed to help with the gameflow in foreign language MMOs, the tool could also be used for everyday work and/or web browsing.

## Prerequisites
* LibreTranslate `(Python >= 3.8 needed)`,
* Cloud Vision API key.

## Setting up the project
1) Install LibreTranslate by running the following commands
``` bash
pip install libretranslate
```
``` bash
libretranslate
```
By default, LibreTranslate is going to run on `localhost:5000`.

2) Obtain your Cloud Vision API key (to be done)
* Head to [Google Cloud Console](https://console.cloud.google.com),
* In the top left corner "Select a project" <img width="300" height="115" alt="select_project" src="https://github.com/user-attachments/assets/74e28f23-d230-4315-ba6e-8927fb041147"/> and press "New project" ![Alt text](images/new_project.png),
* ...


## Running the Overlay
* During the first startup set up the `API key` from Cloud Vision API,
* When launched, the `LibreTranslate` launch script is ran automatically; thus no need to call it manually each time.
* After assigning a `Show Overlay` button, simply press it to display the overlay over your window.
  

