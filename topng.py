from PIL import Image, ImageDraw
img = Image.new('I', (15360+1,15360+1))
draw = ImageDraw.Draw(img)
f = open("131679685152568547.txt")
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

    value = int((z + 100) * (256 * 256) / 1024)
    draw.point((x,y),value)
    i+=1
    if i % (15360+1) == 0:
        j += 1
        print(j)

print("saving..")
img.save("terrain.png")