from Game_class import Game

class Jump() :

    jump_coef = 3.5
    to_jump_max = 0.05

    def handle_jump() :
            for entity in Game.entities:
                 
                if not type(entity).__name__ == "Pawn":
                     continue
                if not entity.is_jumping:continue

                collide = entity.collision.is_colliding_any()
                if collide and "top" in collide:
                    entity.is_jumping = False
                    entity.rect.y = collide[collide.index("top")-1].rect.y + collide[collide.index("top")-1].rect.height
                    continue
                     

                if entity.rect.y <= entity.jump_point :
                    entity.rect.y = entity.jump_point
                    entity.jump_speed = entity.jump_initial_speed
                    entity.is_jumping = False
                else : 
                    to_jump = ((entity.rect.y - entity.jump_point)*100 / entity.jump_height)/100
                    if to_jump >= Jump.to_jump_max:
                        entity.jump_speed = entity.jump_initial_speed * (to_jump*2)
                    else : entity.is_jumping = False
                    entity.rect.y -= entity.jump_speed * Game.dt