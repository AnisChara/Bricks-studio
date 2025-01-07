import math
from Collision_class import Collision

class CircleCollision(Collision): 
    
    def __init__(self, entity, collision_exception):
        super().__init__("circle", entity, collision_exception)
        self.radius = entity.rect.width / 2

    def is_colliding(self, other_entity):
        if other_entity.shape == "circle":
            # Collision entre deux cercles
            dx = self.entity.rect.centerx - other_entity.rect.centerx
            dy = self.entity.rect.centery - other_entity.rect.centery
            distance = math.sqrt(dx**2 + dy**2)
            return distance <= (self.radius + other_entity.collision.radius)

        elif other_entity.shape == "square":
            # Collision entre un cercle et un carré (approximation)
            circle_x = self.entity.rect.centerx
            circle_y = self.entity.rect.centery
            square_x = other_entity.rect.x
            square_y = other_entity.rect.y
            square_width = other_entity.rect.width
            square_height = other_entity.rect.height

            # Trouver le point le plus proche du cercle sur le carré
            closest_x = max(square_x, min(circle_x, square_x + square_width))
            closest_y = max(square_y, min(circle_y, square_y + square_height))

            # Calculer la distance entre le centre du cercle et ce point
            dx = circle_x - closest_x
            dy = circle_y - closest_y
            distance = math.sqrt(dx**2 + dy**2)

            return distance <= self.radius
        
        return False
    
    def get_collision_side(self, other_entity):
        if not self.is_colliding(other_entity):
            return None  # Pas de collision

        dx = self.entity.rect.centerx - other_entity.rect.centerx
        dy = self.entity.rect.centery - other_entity.rect.centery

        if abs(dx) > abs(dy):
            # Collision horizontale (gauche ou droite)
            if dx > 0:
                return "collision-left"  # L'entité actuelle est à droite de l'autre
            else:
                return "collision-right"  # L'entité actuelle est à gauche de l'autre
        else:
            # Collision verticale (haut ou bas)
            if dy > 0:
                return "collision-top"  # L'entité actuelle est en dessous de l'autre
            else:
                return "collision-bottom"  # L'entité actuelle est au-dessus de l'autre