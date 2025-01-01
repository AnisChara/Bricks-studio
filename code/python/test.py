# Example file showing a circle moving on screen
import pygame
import random

# pygame setup
pygame.init()
screen = pygame.display.set_mode((1280, 720))
clock = pygame.time.Clock()
running = True
dt = 0

player_pos = pygame.Vector2(screen.get_width() / 2, screen.get_height() / 2)

while running:
    # poll for events
    # pygame.QUIT event means the user clicked X to close your window
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
 
    screen.fill("pink")
    pygame.draw.rect(screen, "red", (player_pos,(40,40)))

    keys = pygame.key.get_pressed()
    if keys[pygame.K_ESCAPE]: 
      running = False 
    elif keys[pygame.K_a] & keys[pygame.K_c]:
        running = False
        
    elif keys[pygame.K_b] & keys[pygame.K_c]:
        player_pos.y = random.randint(0,700)
        player_pos.x = random.randint(0,1000)
        


    # flip() the display to put your work on screen
    pygame.display.flip()

    # limits FPS to 60
    # dt is delta time in seconds since last frame, used for framerate-
    # independent physics.
    dt = clock.tick(60) / 1000

pygame.quit()