# Example file showing a circle moving on screen
import pygame
from Pawn_class import Pawn
from Game_class import Game

# pygame setup
pygame.init()
Game = Game(pygame.display.set_mode((1280, 720)),pygame.time.Clock())

background = pygame.image.load("C://Users/user/Pictures/wp4470754.webp")

player_pos = pygame.Vector2(Game.screen.get_width() / 2, Game.screen.get_height() / 2)

Player = Pawn(1,1,100,100,100,10,False, "circle")
print(Player.collision.radius)

while Game.running:

    Game.screen.blit(background, (0,0))
    Game.screen.blit(Player.image, Player.rect)

    # poll for events
    # pygame.QUIT event means the user clicked X to close your window
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            Game.running = False
 

    keys = pygame.key.get_pressed()
    if keys[pygame.K_ESCAPE]: 
      Game.running = False 
    elif keys[pygame.K_m]:
      Game.running = False
    if keys[pygame.K_d]:
      Player.rect.x += 10
    if keys[pygame.K_q]:
      Player.rect.x -= 10
    if keys[pygame.K_s]:
      Player.rect.y += 10
    if keys[pygame.K_z]:
      Player.rect.y -= 10


    # flip() the display to put your work on screen
    pygame.display.flip()

    # limits FPS to 60
    # dt is delta time in seconds since last frame, used for framerate-
    # independent physics.
    Game.dt = Game.clock.tick(60) / 1000

pygame.quit()