
# YOLOLA
YOLO Label Assist is a tool to help you create labeled images with the help of a trained model.
This way you can increase your training set without having to manually mark each object.
Instead you only have to fix wrong ones.

## Usage

 - in `config.json` enter the process name you want to take screenshots of.
 - in `/trainfiles/` place your .cfg, .names and .weights files of your current model, named
   after the process name
 - start YOLOLA
 - record screenshots
 - possible modes:
	 - Prediction: use the predicted boxes
	 - AllSelectedType: use the predicted boxes, but change the type to the selected one.
	 - NoObjects: save with no boxes
 - open [LabelImg](https://github.com/tzutalin/labelImg) in the `/img/` folder and correct the boxes
   
   
## Credits
This tool is based on [this programm by Trombov](https://github.com/Trombov/FutureNNAimbot)
