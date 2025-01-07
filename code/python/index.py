# Example file showing a circle moving on screen
import pygame
from Pawn_class import Pawn
from Game_class import Game
from Gravity_class import Gravity
from Jump_class import Jump
from Structure_class import Structure
from Entity_class import Entity

# pygame setup
pygame.init()

background = pygame.image.load("C://Users/user/Pictures/wp4470754.webp")

Player = Pawn("sdq",0,0,100,100,100,10,True, "square",10)
left_border = Entity("left_border",-1,-1,-2147483646,2147483646,0,True,"square",0,False)
right_border = Entity("right_border",Game.screen_width+1,-1,2147483646, 2147483646,0,True,"square",0,False)
up_border = Entity("up_border",-1,-1,2147483646, -2147483646,0,True,"square",0,False)
down_border = Entity("down_border",-1,Game.screen_height+1,2147483646, 2147483646,0,True,"square",0,False)

Game.entities.append(Player)
Game.entities.append(left_border)
Game.entities.append(right_border)
Game.entities.append(down_border)

while Game.running:

    Game.screen.blit(background, (0,0))
    Game.render()

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            Game.running = False
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_SPACE:
                Player.jump(400)

    keys = pygame.key.get_pressed()
    if keys[pygame.K_ESCAPE]: 
      Game.running = False 
    elif keys[pygame.K_m]:
      Game.running = False
    if keys[pygame.K_d]:
      Player.rect.x += 300 * Game.dt
    if keys[pygame.K_q]:
      Player.rect.x -= 300 * Game.dt
    if keys[pygame.K_z]:
      Player.rect.y = 0
    if keys[pygame.K_w]:
       print(Player.is_jumping)
    

    Jump.handle_jump()
    Gravity.fall()



    # flip() the display to put your work on screen
    pygame.display.flip()

    # limits FPS to 60
    # dt is delta time in seconds since last frame, used for framerate-
    # independent physics.
    Game.dt = Game.clock.tick(60) / 1000

pygame.quit()