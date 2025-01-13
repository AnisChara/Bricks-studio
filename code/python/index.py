import pygame
from Pawn_class import Pawn
from Game_class import Game
from Gravity_class import Gravity
from Jump_class import Jump
from Structure_class import Structure
from Entity_class import Entity
from Projectile_class import Projectile
from OffensiveStructure_class import Offensive_Structure
from OffensiveEntity_class import Offensive_Entity

# pygame setup
pygame.init()

background = pygame.image.load("C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/Fond.jpg")

left_border = Structure("left_border",-2147483646,-1,2147483646,2147483646,0,True,"square",0,False)
right_border = Structure("right_border",Game.screen_width+1,-1,2147483646, 2147483646,0,True,"square",0,False)
up_border = Structure("up_border",-1,-2147483646,2147483646, 2147483646,0,True,"square",0,False)
down_border = Structure("down_border",-1,Game.screen_height+1,2147483646, 2147483646,0,True,"square",0,False)
Player = Pawn("Player",100,0,100,100,100,10,True, "square",10)
Player.set_weapon()

Game.entities.append(left_border)
Game.entities.append(right_border)
Game.entities.append(up_border)
Game.entities.append(down_border)
Game.entities.append(Player)
entities = Game.load_entities()

for entity in entities:
    if entity['type'] == 'structure':
        Game.entities.append(Structure(entity['id'], entity['x']/0.625, entity['y']/0.625, entity['width']/0.625, entity['height']/0.625,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render']))
    if entity['type'] == 'offensive_structure':
        Game.entities.append(Offensive_Structure(entity['id'], entity['x']/0.625, entity['y']/0.625, entity['width']/0.625, entity['height']/0.625,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render']))
    if entity['type'] == 'offensive_entity':
        Game.entities.append(Offensive_Entity(entity['id'], entity['x']/0.625, entity['y']/0.625, entity['width']/0.625, entity['height']/0.625,entity["max_health"],entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render']))
    if entity['type'] == 'zone':
        Game.entities.append(Structure(entity['id'], entity['x']/0.625, entity['y']/0.625, entity['width']/0.625, entity['height']/0.625,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render']))
 
while Game.running:

    Game.screen.blit(background, (0,0))
    Game.render()
    Game.reset_keys()

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            Game.running = False
        if event.type == pygame.KEYDOWN:
            Game.keydown_handle(event)
            

    keys = pygame.key.get_pressed()
    if keys[pygame.K_ESCAPE]: 
      Game.running = False 
    if keys[pygame.K_d]:
        Player.move('right', 30)
        
    if keys[pygame.K_q]:
        Player.move('left', 30)
        
    if keys[pygame.K_s]:
        Player.move('bottom', 30)
        
    if Game.keys.A == True:
        Player.weapon.shoot(50,50,20,10,None,0)
        
    if Game.keys.ESPACE == True:
        Player.jump(300)
        

    Jump.handle_jump()
    Gravity.fall()
    Projectile.handle_projectile()
    Game.check_alive()
    Offensive_Structure.handle_offensive_structures()
    Offensive_Entity.handle_offensive_entities()


    # flip() the display to put your work on screen
    pygame.display.flip()

    # limits FPS to 60
    # dt is delta time in seconds since last frame, used for framerate-
    # independent physics.
    Game.dt = Game.clock.tick(60) / 1000

pygame.quit()