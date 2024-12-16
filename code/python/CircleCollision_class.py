from Collision_class import Collision

class CircleCollision(Collision): 
    
    def __init__(self, entity, collision_exception):
        super().__init__("circle", entity, collision_exception)
        self.radius = entity.rect.width / 2

    def is_colliding(self, other_entity):
        #TODO
        pass