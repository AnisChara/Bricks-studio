from Pawn_class import Pawn

class Ennemy(Pawn) :

    def __init__(self, x, y, width, height, health, velocity) : #ajouter arg pour image
        super().__init__(x, y, width, height,health)
        self.pattern = []

    def follow_pattern(self) :
        pass