﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingSpawn : MobSpawn
{
	private static UnityEngine.Object[] viking_objects;
	
	public override GameObject spawn(Transform transformation){
		type = EntityClass.VikingSpawn;
		GameObject mob = Instantiate(viking_objects[Random.Range(0, viking_objects.Length)], transformation.position, transformation.rotation) as GameObject;
		mob.AddComponent<MobAI>().path = this.path;
		mob.GetComponent<MobAI>().is_hostile = this.is_hostile;
		this.spawned_mobs.Add(mob);
		return mob;
	}

	void Start()
	{
		viking_objects = Resources.LoadAll("Vikings");
		base.type = type;
	}
}
