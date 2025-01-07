import pygame

class Game() :

    entities = []
    screen_width = 1280
    screen_height = 720
    screen = pygame.display.set_mode((screen_width, screen_height))
    clock = pygame.time.Clock()
    running = True
    dt = 0

    def render():
        for entity in Game.entities:
            if entity.render:
                Game.screen.blit(entity.image, entity.rect)
        