extends Node2D
class_name BoardTile

# Emite quando a Peça é movida para fora do Tile
signal remove_piece 

# Emite quando a Peça é movida para este Tile
signal add_piece

var piece = {
	"exists": false,
	"piece": null 
}

func _ready():
	pass 
	
func get_tile_pos() -> Vector2:
	return position #TODO
	
func set_tile_pos(new_position: Vector2) -> void:
	position = new_position
	
func remove_piece() -> void:
	piece["exists"] = false
	piece["piece"] = null
	
	emit_signal("remove_piece")
	
func set_piece(piece) -> void:
	piece["exists"] = true
	piece["piece"] = piece
	
	emit_signal("add_piece")
