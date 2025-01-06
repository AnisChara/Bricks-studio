import pygame
import uuid
from CircleCollision_class import CircleCollision
from SquareCollision_class import SquareCollision
from Emitter_class import Emitter

class Entity(pygame.sprite.Sprite) :

    def __init__(self,id, x, y, width, height, speed = 1, is_collidable = False, shape = "square", weight = 10 ) : #ajouter arg pour image
        super().__init__()
        self.id = id
        self.image = pygame.image.load("C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/mario.png")
        self.image = pygame.transform.scale(self.image,(width, height))
        self.rect = self.image.get_rect()
        self.rect.x = x
        self.rect.y = y
        self.rect.width = width
        self.rect.height = height
        self.shape = shape
        if shape == "circle":
            self.collision = CircleCollision(self, [])
        if shape == "square":
            self.collision = SquareCollision(self, [])
        # self.gravity = Gravity()
        # self.jump = Jump()
        # self.weapon = Emitter()
        self.is_collidable = is_collidable
        self.speed = speed
        self.direction = ""
        self.weight = weight
        self.is_jumping = False
        self.is_falling = False
        self.falling_speed = 0



    def set_weapon(self):
        self.weapon = Emitter()