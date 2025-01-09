from Entity_class import Entity
from Jump_class import Jump

class Pawn(Entity) :

    def __init__(self,id, x, y, width, height, health = 10, speed = 1, is_collidable = False, shape = "square", weight = 10 ) : #ajouter arg pour image
        super().__init__(id,x, y, width, height,  speed, is_collidable, shape, weight)
        self.max_health = health
        self.health = health
        

    def take_damage(self, damage):
        self.health -= damage

    def jump(self,jump_height):
        if not (self.is_jumping) and not (self.is_falling):
            self.jump_height = jump_height + jump_height*Jump.to_jump_max
            self.jump_initial_speed =  Jump.jump_coef * jump_height
            self.jump_point = 0
            self.jump_speed = self.jump_initial_speed
            self.is_jumping = True
            self.jump_point = self.rect.y - jump_height
        