using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class LandGen : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] Corners;
    public GameObject[] Edges;
    public GameObject[] Centers;
    public GameObject map;
    
    public int array_size;
    public Vector3[] position;
    private int candidate_indexes;
    private int used_indexes;
    public float part_size;
    private NavMeshSurface navmesh;

    
    
    private void CornerGen1(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Corners[Random.Range(0, Corners.Length)], new Vector3(i, height, j) , Quaternion.Euler(0, 270, 0),map.transform);
       
    }

    private void CornerGen2(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Corners[Random.Range(0, Corners.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 180, 0),map.transform);
        
    }

    private void CornerGen3(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Corners[Random.Range(0, Corners.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 0, 0),map.transform);
    }

    private void CornerGen4(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Corners[Random.Range(0, Corners.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 90, 0),map.transform);
    }

    private void EdgeR(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Edges[Random.Range(0, Edges.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 180, 0),map.transform);

    }

    private void EdgeL(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Edges[Random.Range(0, Edges.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 0, 0),map.transform);

    }

    private void EdgeT(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Edges[Random.Range(0, Edges.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 270, 0),map.transform);

    }

    private void EdgeB(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Edges[Random.Range(0, Edges.Length)], new Vector3(i, height, j), Quaternion.Euler(0, 90, 0),map.transform);

    }

    private void Middle(float i, float j)
    {
        int height = Random.Range(20, 100);
        Instantiate(Centers[Random.Range(0, Centers.Length)], new Vector3(i, height, j), Quaternion.Euler(Random.Range(1, 4) * new Vector3(0, 90, 0)),map.transform);

    }

    IEnumerator Start()
    {
        map = Instantiate(map, new Vector3(part_size * array_size / 2, 0, part_size * array_size / 2),Quaternion.identity);
        navmesh = map.GetComponent<NavMeshSurface>();
        
        float posxl=0;
        float posx= 0;
        float posz = 0;
        CornerGen1(posx+part_size/2, posz+part_size/2);
        posx += part_size;
        for (float positx = posx; positx < (array_size - 1) * part_size; positx = positx + part_size)
        {
            EdgeT(positx + part_size / 2, posz + part_size / 2);
            posxl = positx;
        }
        posx = posxl;
        CornerGen2(posx+part_size*3/2, posz + part_size / 2);
        for(int i = 0; i < array_size - 2; i++)
        {
            posz = (i + 1) * part_size;
            EdgeL(part_size / 2, posz+part_size/2);
            for (int k = 0; k < array_size - 2; k++)
            {
                posz = (i + 1) * part_size+part_size/2;
                posx= (k + 1) * part_size+part_size/2;
                Middle(posx, posz);

            }
            EdgeR(posx + part_size, posz);
        }
        posx = part_size/2;
        posz += part_size;
        CornerGen3(posx, posz);
        posx += part_size;
        for (float positx = posx; positx <= (array_size - 1) * part_size; positx = positx + part_size)
        {
            EdgeB(positx, posz);
            posx = positx;
        }
        CornerGen4(posx+part_size, posz);
        
        yield return new WaitForSeconds(5f);
        foreach (var player in players)
        {
            Instantiate(player, map.transform.position, Quaternion.identity);
        }
        navmesh.BuildNavMesh();
    }
}