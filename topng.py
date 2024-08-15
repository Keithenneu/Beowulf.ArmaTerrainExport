from PIL import Image, ImageDraw
import math

filename = "133682323533037935"
num_lines = 0
### Get the line count first, I guess
with open(filename + ".txt", "rb") as f:
    num_lines = sum(1 for _ in f)

print(num_lines)

### This will allow us to get the worldsize without having to hardcode it
### Arma 3 map is always squared so sqrt of lines will be worldsize
### Would be -1 to get the worldsize because we go from 0,0 to worldSize,worldSize
### However since we need the points and not the distance we don't do -1

worldsize = math.sqrt(num_lines)

### We could use pandas to easily find the min and max here. Unfortunately we need it
### I really don't want to load the whole file into memory and chunking is difficult
### We'll just read the file AGAIN and only go for z values and find the min max.
z_min = 0.0
z_max = 0.0
f = open(filename + ".txt")
i=0
j=0
while True:
    line = f.readline()
    if len(line) == 0:
        break
    parts = line.split(" ")
    z = float(parts[2])
    if ((i == 0) & (j == 0)):
        z_min = z
        z_max = z
    else:
        if z < z_min:
            z_min = z
            print("z_min: " + str(z_min))
        elif z > z_max:
            z_max = z
            print("z_max: " + str(z_max))
        else:
            pass
    i+=1
    if i % (worldsize) == 0:
        j += 1

### Now that we have z_min and z_max we can normalise the heights for greyscale

img = Image.new('I', (int(worldsize), int(worldsize))) 
draw = ImageDraw.Draw(img)
f = open(filename + ".txt")
i=0
j=0
while True:
    line = f.readline()
    if len(line) == 0:
        break
    parts = line.split(" ")
    x = int(parts[0])
    y = int(parts[1])
    z = float(parts[2])

    ### Probably the biggest change in the script, now we can dynamically set the scale
    scale_factor = 65535 / (z_max - z_min)
    value = int((z - z_min) * scale_factor)

    ### One last change is here
    ### Arma starts counting 0,0 in the bottom left going up
    ### Pillow has 0,0 in the top left going down
    ### This will result in an inverted y-axis
    ### Needs to subtract y from our value worldsize to fix this
    draw.point((x,worldsize-y),value)
    i+=1
    if i % (worldsize) == 0:
        j += 1
        print(j)

print("saving..")
img.save(filename + ".png")