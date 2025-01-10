import uuid
from Game_class import Game
class Emitter() :

    def __init__(self, shooter) : #ajouter arg pour image
        self.shooter = shooter
        self.x = self.shooter.rect.x + self.shooter.rect.width
        self.y = self.shooter.rect.y + self.shooter.rect.height / 2

    def update_position(self):
        if self.shooter.direction == 'right':
            self.x = self.shooter.rect.x + self.shooter.rect.width
            self.y = self.shooter.rect.y + self.shooter.rect.height / 2
        elif self.shooter.direction == 'left':
            self.x = self.shooter.rect.x
            self.y = self.shooter.rect.y + self.shooter.rect.height / 2
        elif self.shooter.direction == 'top':
            self.x = self.shooter.rect.x + self.shooter.rect.width / 2
            self.y = self.shooter.rect.y
        elif self.shooter.direction == 'bottom':
            self.x = self.shooter.rect.x + self.shooter.rect.width / 2
            self.y = self.shooter.rect.y + self.shooter.rect.height

    def shoot(self,projectile_width,projectile_height,projectile_speed,projectile_damage,projectile_direction = None,projectile_weight = 0): #parametre de direction
        self.update_position()
        from Projectile_class import Projectile

        if projectile_direction != None : self.shooter.direction = projectile_direction

        if self.shooter.direction == 'right':
            projectile_x = self.x
            projectile_y = self.y-projectile_height/2
        if self.shooter.direction == 'left':
            projectile_x = self.x - projectile_width
            projectile_y = self.y-projectile_height/2
        if self.shooter.direction == 'top':
            projectile_x = self.x-projectile_width/2
            projectile_y = self.y - projectile_height
        if self.shooter.direction == 'bottom':
            projectile_x = self.x-projectile_width/2
            projectile_y = self.y

        projectile = Projectile(uuid.uuid4(),projectile_x,projectile_y,projectile_width,projectile_height,projectile_speed,True,"square",projectile_weight,self,self.shooter.direction,projectile_damage)
        Game.entities.append(projectile)