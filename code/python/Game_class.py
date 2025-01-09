import pygame
from Keys_class import Keys

class Game() :

    entities = []
    screen_width = 1280
    screen_height = 720
    screen = pygame.display.set_mode((screen_width, screen_height))
    clock = pygame.time.Clock()
    running = True
    dt = 0
    keys = Keys()

    def render():
        for entity in Game.entities:
            if entity.render:
                Game.screen.blit(entity.image, entity.rect)
        
    def reset_keys():
        
        for key in vars(Game.keys):
            setattr(Game.keys, key, False)