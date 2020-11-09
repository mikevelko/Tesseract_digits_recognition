# Winforms app to recognize text from image
## Description:
This app could recognize text (english) and digits from images and Encapsulated PostScript files.
Gives you an ability to change a threshold coefficient for better recognition.
## PLEASE INSTALL GHOSTSCRIPT FIRST
- https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs9533/gs9533w64.exe  
It needs to open ```.eps``` files
## Requirements to run project:
- Make sure you are on Windows now and have installed Visual Studio and .NET Framewok 4.8
- Open ```Tesseract WinForms.sln``` with Visual Studio
- and run project (F5)
### How it works:
- Upload your picture ```(.png, .jpg)```
- Or you can upload ```(.eps)``` (make sure you have installed GhostScript by link above)
- You can set the threshold coefficient that removes noizes and pixels that are not a text from picture
- Set ```--psm N``` option (mostly option number 6 works better than others). Set Tesseract to only run a subset of layout analysis and assume a certain form of image. 
- The options for N are:
  - ```0``` = Orientation and script detection (OSD) only.
  - ```1``` = Automatic page segmentation with OSD.
  - ```2``` = Automatic page segmentation, but no OSD, or OCR.
  - ```3``` = Fully automatic page segmentation, but no OSD. (Default)
  - ```4``` = Assume a single column of text of variable sizes.
  - ```5``` = Assume a single uniform block of vertically aligned text.
  - ```6``` = Assume a single uniform block of text.
  - ```7``` = Treat the image as a single text line.
  - ```8``` = Treat the image as a single word.
  - ```9``` = Treat the image as a single word in a circle.
  - ```10``` = Treat the image as a single character.
               

