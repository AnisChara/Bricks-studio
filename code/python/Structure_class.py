from Entity_class import Entity

class Structure(Entity) :

    def __init__(self, x, y, width, height) : #ajouter arg pour image
        super().__init__(x, y, width, height)
        self.pattern = []

    def follow_pattern(self) :
        #TODO
        pass
    def interact(self):
        #TODO
        pass