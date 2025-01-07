from Entity_class import Entity

class Structure(Entity) :

    def __init__(self,id, x, y, width, height, speed = 1, is_collidable = False, shape = "square", weight = 10, render = True ) : #ajouter arg pour image
        super().__init__(id,x, y, width, height,  speed, is_collidable, shape, weight, render)
        self.pattern = []

    def follow_pattern(self) :
        #TODO
        pass
    def interact(self):
        #TODO
        pass