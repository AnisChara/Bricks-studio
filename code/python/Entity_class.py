import pygame
from CircleCollision_class import CircleCollision
from SquareCollision_class import SquareCollision
from Emitter_class import Emitter
from Game_class import Game

class Entity(pygame.sprite.Sprite) :

    def __init__(self,id,image, x, y, width, height, speed = 1, is_collidable = True, shape = "square", weight = 10,render = True ) : #ajouter arg pour image
        super().__init__()
        self.id = id
        self.render = render
        if render:
            self.image = pygame.image.load(image)
            self.image = pygame.transform.scale(self.image,(width, height))
            self.rect = self.image.get_rect()
            self.rect.x = x
            self.rect.y = y
        else : 
            self.rect = pygame.Rect(x, y, width, height)
            self.rect.x = x
            self.rect.y = y
        self.shape = shape
        if shape == "circle":
            self.collision = CircleCollision(self, [])
        if shape == "square":
            self.collision = SquareCollision(self, [])
        self.is_collidable = is_collidable
        self.speed = speed
        self.direction = "right"
        self.weight = weight
        self.is_jumping = False
        self.is_falling = False
        self.falling_speed = 0
        self.sens = "right"



    def set_weapon(self):
        self.weapon = Emitter(self)

    def die(self):
        try:
            Game.entities.remove(self)
        except ValueError:
            return
    

    def move(self,direction,speed = None):

        if speed is None: speed = self.speed*10
        else: speed *= 10

        collide = self.collision.is_colliding_any()
        
        if direction == "left":
            if not (collide and direction in collide):
                self.rect.x -= speed * Game.dt
                if not self.direction == direction:
                    self.direction = direction
                    if self.sens == 'right' : 
                        self.image = pygame.transform.flip(self.image, True, False)
                        self.sens = 'left'
            else: 
                self.rect.x = collide[collide.index(direction)-1].rect.x + collide[collide.index(direction)-1].rect.width+1
                return False
        if direction == "right":
            if not (collide and direction in collide):
                self.rect.x += speed * Game.dt
                if not self.direction == direction:
                    self.direction = direction
                    if self.sens == 'left' :
                        self.image = pygame.transform.flip(self.image, True, False)
                        self.sens = 'right'
            else:
                self.rect.x = collide[collide.index(direction)-1].rect.x-1 - self.rect.width
                return False
        if direction == "top":
            if not (collide and direction in collide):
                self.rect.y -= speed * Game.dt
                self.direction = direction
            else :
                self.rect.y = collide[collide.index(direction)-1].rect.y + collide[collide.index(direction)-1].rect.height+1
                return False
        if direction == "bottom":
            if not (collide and direction in collide):
                self.rect.y += speed * Game.dt
                self.direction = direction
            else:
                self.rect.y = collide[collide.index(direction)-1].rect.y-self.rect.height-1
                return False