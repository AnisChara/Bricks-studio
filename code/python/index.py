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
from loading_function import instance_entities

# pygame setup
pygame.init()

level = 0

def next_level(Player,level):
    level = level +1
    Game.clean_entities()
    if level == level_max :return Player,level
    Player = instance_entities(entities, level)
    return Player,level

GameBackground = pygame.image.load("C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/Bricks_Interfaces/bin/Debug/net8.0-windows/../../../Assets/Fond.jpg")
GameBackground = pygame.transform.scale(GameBackground,(1280,720))

VictoryScreen = pygame.image.load("C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/Bricks_Interfaces/bin/Debug/net8.0-windows/../../../Assets/Victoire.png")
VictoryScreen = pygame.transform.scale(VictoryScreen,(1280,720))

left_border = Structure("left_border",-2147483646,-1,2147483646,2147483646,0,True,"square",0,False)
right_border = Structure("right_border",Game.screen_width+1,-1,2147483646, 2147483646,0,True,"square",0,False)
up_border = Structure("up_border",-1,-2147483646,2147483646, 2147483646,0,True,"square",0,False)
down_border = Structure("down_border",-1,Game.screen_height+1,2147483646, 2147483646,0,True,"square",0,False)

Game.entities.append(left_border)
Game.entities.append(right_border)
Game.entities.append(up_border)
Game.entities.append(down_border)
entities = Game.load_entities()
level_max = len(entities)
Gravity.is_gravity = False

Player = instance_entities(entities,level)
background = GameBackground

while Game.running:
    
    if (level == level_max):
        background = VictoryScreen
        if Game.keys.ESPACE == True:
            background = GameBackground
            level = 0
            Player = instance_entities(entities, level)


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
    if keys[pygame.K_q]:
        Player.move('left', 40)
        
    if keys[pygame.K_s]:
        Player.move('bottom', 40)
        
    if Game.keys.A == True:
        Player.weapon.shoot(50,50,70,50,'top',0)
        
    if keys[pygame.K_z]:
        Player.move('top', 40)
        
    if keys[pygame.K_d]:
        Player.move('right', 40)
        
    if Player.finish() == True:
        if level < level_max:
            Player,level = next_level(Player,level)


    Jump.handle_jump()
    Gravity.fall()
    Projectile.handle_projectile()
    Game.check_alive()
    Offensive_Structure.handle_offensive_structures()
    Offensive_Entity.handle_offensive_entities()
    Offensive_Entity.HandleTrackPlayer(Player)

    try:
        if Player.health <= 0:
            Game.clean_entities()
            Player = instance_entities(entities,level)
    except AttributeError:
        pass

    pygame.display.flip()
    Game.dt = Game.clock.tick(60) / 1000

pygame.quit()