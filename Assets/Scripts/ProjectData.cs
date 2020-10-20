using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectData : ScriptableObject
{

    protected string projectName;
    protected char projectId;
    protected int ownerId;

    protected string[] skills;

    public ProjectData(string _pName, char _pId, int _oId, string[] _skills)
    {
        projectName = _pName;
        projectId = _pId;
        ownerId = _oId;

        skills = new string[_skills.Length];
        for (int i = 0; i < _skills.Length; i++)
        {
            skills[i] = _skills[i];
        }
    }
}
