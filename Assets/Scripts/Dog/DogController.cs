using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float followSpeed;

    private Transform _playerPos;
    public Vector3 offset;
    private bool _canFollow;


    private void Update()
    {
        if (_canFollow)
        {
            transform.position = Vector3.Lerp(transform.position, _playerPos.position,followSpeed * Time.deltaTime);
        }
    }
    public void Initialize(Transform position)
    {
        _playerPos = position;
        _canFollow = true;
        transform.position = offset + _playerPos.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canFollow = false;
        }
    }
}
