from Entity_class import Entity

class Pawn(Entity) :

    def __init__(self, x, y, width, height, health = 10, speed = 1, is_collidable = False, shape = "square") : #ajouter arg pour image
        super().__init__(x, y, width, height,  speed, is_collidable, shape)
        self.max_health = health
        self.health = health

    def take_damage(self, damage):
        pass#TODO