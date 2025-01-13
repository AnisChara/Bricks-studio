from Entity_class import Entity

class Structure(Entity) :

    def __init__(self,id, x, y, width, height, speed = 1, is_collidable = False, shape = "square", weight = 10, render = True, image="C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/téléchargement.jpg"  ) : #ajouter arg pour image
        super().__init__(id,image,x, y, width, height,  speed, is_collidable, shape, weight, render)
        self.pattern = []

    def follow_pattern(self) :
        #TODO
        pass
    def interact(self):
        #TODO
        pass