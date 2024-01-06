using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {
    [SerializeField]
    protected int poleCount = 3;

    [SerializeField]
    private int diskCount = 4;

    [SerializeField]
    private Pole polePrefab;

    [SerializeField]
    private Disk diskPrefab;

    [SerializeField]
    private Vector3 _diskOffset;

    private Pole[] _poles;

    [SerializeField]
    private float poleSpacing;

    [SerializeField]
    [Range(0.1f, 1.99f)]
    private float diskScaling;

    public Vector3 diskOffset {
        get => _diskOffset;
        private set => _diskOffset = value;
    }

    protected Pole[] poles {
        get => _poles;
        set => _poles = value;
    }

    public virtual void Awake() {
        poles = new Pole[poleCount];
        for (int i = 0; i < poleCount; i++) {
            poles[i] = Instantiate(polePrefab);
            poles[i].transform.position = Vector3.right * (i * (4 + poleSpacing));
        }
    }

    private void Start() {
        for (int i = 0; i < diskCount; i++) {
            Disk disk = Instantiate(diskPrefab, poles[0].transform);
            disk.scale = 4 - i * diskScaling;
            disk.size = i;
            poles[0].PutDisk(disk);
        }

        int center = poleCount / 2;
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = poles[center].transform.position.x;
        Camera.main.transform.position = camPos;
    }
}