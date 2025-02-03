from Pawn_class import Pawn
from Game_class import Game

class Offensive_Entity(Pawn) :

    def __init__(self,id,x, y, width, height, health,speed = 1, is_collidable = False, shape = "square", weight = 10, render = True ,damage = 100, image =  "C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/monster.png",track = False) : #ajouter arg pour image
        super().__init__(id, x, y, width, height, health, speed, is_collidable, shape , weight, image)
        self.damage = damage
        self.track = track

    def deal_damage(self, target):
        target.health -= self.damage

    def handle_offensive_entities():

        for offensive_Entity in Game.entities:
            if not type(offensive_Entity).__name__ == "Offensive_Entity": continue

            collide = offensive_Entity.collision.is_colliding_any()
            if collide:

                for collision in collide:

                    if type(collision).__name__ == "str": continue
                    if type(collision).__name__ == "Offensive_Entity": continue

                    try:
                        offensive_Entity.deal_damage(collision)
                    except AttributeError:
                        pass
    def get_direction_to(self, Player) -> list:
        dx = Player.rect.x - self.rect.x
        dy = Player.rect.y - self.rect.y

        directions = []

        # Déterminer la direction principale (celle avec la plus grande distance)
        if abs(dx) > abs(dy):  
            directions.append("right" if dx > 0 else "left")
            directions.append("bottom" if dy > 0 else "top")
        else:
            directions.append("bottom" if dy > 0 else "top")
            directions.append("right" if dx > 0 else "left")

        return directions

    
    def HandleTrackPlayer(Player):
        for Monster in Game.entities:
            if not type(Monster).__name__ == "Offensive_Entity": continue
            if Monster.track == False: continue

            direction = Monster.get_direction_to(Player)
            if (Monster.move(direction[0]) == False):
                Monster.move(direction[1])