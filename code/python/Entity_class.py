import pygame
import uuid
from CircleCollision_class import CircleCollision
from SquareCollision_class import SquareCollision
from Emitter_class import Emitter

class Entity(pygame.sprite.Sprite) :

    def __init__(self,id, x, y, width, height, speed = 1, is_collidable = True, shape = "square", weight = 10,render = True) : #ajouter arg pour image
        super().__init__()
        self.id = id
        self.render = render
        if render:
            self.image = pygame.image.load("C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/mario.png")
            self.image = pygame.transform.scale(self.image,(width, height))
            self.rect = self.image.get_rect()
        else : 
            self.rect = pygame.Rect(x, y, width, height)
        self.shape = shape
        if shape == "circle":
            self.collision = CircleCollision(self, [])
        if shape == "square":
            self.collision = SquareCollision(self, [])
        self.is_collidable = is_collidable
        self.speed = speed
        self.direction = ""
        self.weight = weight
        self.is_jumping = False
        self.is_falling = False
        self.falling_speed = 0



    def set_weapon(self):
        self.weapon = Emitter()

    def move(self,direction,speed):

        if speed is None: speed = self.speed

        if direction == "left":
            self.rect.x -= speed
        if direction == "right":
            self.rect.x += speed
        if direction == "up":
            self.rect.y -= speed
        if direction == "down":
            self.rect.y += speed