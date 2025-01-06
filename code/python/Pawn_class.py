from Entity_class import Entity

class Pawn(Entity) :

    def __init__(self,id, x, y, width, height,jump_height, health = 10, speed = 1, is_collidable = False, shape = "square", weight = 10) : #ajouter arg pour image
        super().__init__(id,x, y, width, height,  speed, is_collidable, shape, weight)
        self.max_health = health
        self.health = health
        self.jump_height = jump_height

    def take_damage(self, damage):
        pass#TODO

    def jump(self):
        pass
        