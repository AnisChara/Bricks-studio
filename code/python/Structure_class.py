from Entity_class import Entity
from Game_class import Game

class Structure(Entity) :

    def __init__(self,id, x, y, width, height, speed = 1, is_collidable = False, shape = "square", weight = 10, render = True, image= Game.directory+"/Assets/téléchargement.jpg" ,zone = False ) : #ajouter arg pour image
        super().__init__(id,image,x, y, width, height,  speed, is_collidable, shape, weight, render)
        self.pattern = []
        self.zone = zone

    def follow_pattern(self) :
        #TODO
        pass
    def interact(self):
        #TODO
        pass