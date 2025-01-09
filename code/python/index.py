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

left_border = Entity("left_border",-2147483646,-1,2147483646,2147483646,0,True,"square",0,False)
right_border = Entity("right_border",Game.screen_width+1,-1,2147483646, 2147483646,0,True,"square",0,False)
up_border = Entity("up_border",-1,-2147483646,2147483646, 2147483646,0,True,"square",0,False)
down_border = Entity("down_border",-1,Game.screen_height+1,2147483646, 2147483646,0,True,"square",0,False)
Player = Pawn("Player",100,0,100,100,100,10,True, "square",10)


Game.entities.append(left_border)
Game.entities.append(right_border)
Game.entities.append(up_border)
Game.entities.append(down_border)
Game.entities.append(Player)

while Game.running:

    Game.screen.blit(background, (0,0))
    Game.render()
    Game.reset_keys()

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            Game.running = False
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_SPACE:
                Game.keys.SPACE = True
            if event.key == pygame.K_RETURN:
                Game.keys.RETURN = True
            

    keys = pygame.key.get_pressed()
    if keys[pygame.K_ESCAPE]: 
      Game.running = False 
    if keys[pygame.K_d]:
      Player.move('right', 400)
    if keys[pygame.K_q]:
      Player.move('left', 400)
    if Game.keys.SPACE == True:
      Player.jump(300)
    if keys[pygame.K_w]:
       print(Player.collision.is_colliding_any())


    Jump.handle_jump()
    Gravity.fall()



    # flip() the display to put your work on screen
    pygame.display.flip()

    # limits FPS to 60
    # dt is delta time in seconds since last frame, used for framerate-
    # independent physics.
    Game.dt = Game.clock.tick(120) / 1000

pygame.quit()