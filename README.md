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
* In the top left corner "Select a project"

<img width="300" height="115" alt="select_project" src="https://github.com/user-attachments/assets/74e28f23-d230-4315-ba6e-8927fb041147"/>
and press "New project"

<img width="219" height="118" alt="new_project" src="https://github.com/user-attachments/assets/3fd56228-2003-4e2c-a839-f0b3a821ec63" />

* Insert your project name and press "Next" and a pop up will appear, on which you simply select your project
<img width="319" height="138" alt="select_created_project" src="https://github.com/user-attachments/assets/4cb5940d-4f97-4d18-b339-046a84aefc50" />

* In bar on top of the website, look up `Cloud Vision API` and enable it
<img width="585" height="116" alt="type_cloud_vision_api" src="https://github.com/user-attachments/assets/997a74c3-34a8-470b-9c06-315e7d59dbfb" />
<img width="286" height="202" alt="enable_cloud vision_for_the_project" src="https://github.com/user-attachments/assets/66f3078e-c680-4f54-b6d0-662ae329960a" />

* In the top left corner, head over to "Credentials" tab and create new credentials
<img width="223" height="159" alt="head_over_to_credentials" src="https://github.com/user-attachments/assets/31ebea4d-7bb3-4d17-99c3-327afd72821e" />
<img width="406" height="96" alt="create_credentials" src="https://github.com/user-attachments/assets/daade17d-233f-49df-80ed-df24a1dbd452" />

* name your API key and in the textbox search for `Cloud Vision API`,
<img width="440" height="621" alt="select_cloud_vision_api" src="https://github.com/user-attachments/assets/9221e625-ff4d-490c-83d9-fdc933a93715" />

* Copy and save your `API Key` and paste it during the launch of the `OCR Translator Overlay`
<img width="443" height="137" alt="copy_your_apikey" src="https://github.com/user-attachments/assets/399e0386-3072-4387-ab66-66324f30997c" />


## Running the Overlay
* During the first startup set up the `API key` from Cloud Vision API,
* When launched, the `LibreTranslate` launch script is ran automatically; thus no need to call it manually each time.
* After assigning a `Show Overlay` button, simply press it to display the overlay over your window.
  

