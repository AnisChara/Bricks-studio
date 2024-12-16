from Collision_class import Collision

class SquareCollision(Collision): 
    
    def __init__(self, entity, collision_exception):
        super().__init__("square", entity, collision_exception)

    def is_colliding(self, other_entity):
        #TODO
        return