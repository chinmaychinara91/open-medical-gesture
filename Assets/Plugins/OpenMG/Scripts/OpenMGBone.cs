/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using UnityEngine;

namespace OpenMGHandModel
{
    public class OpenMGBone
    {
        public enum BoneType
        {
            TYPE_INVALID = -1,
            TYPE_METACARPAL = 0,
            TYPE_PROXIMAL = 1,
            TYPE_INTERMEDIATE = 2,
            TYPE_DISTAL = 3
        }

        public BoneType Type;

        public OpenMGBone()
        {
            Type = BoneType.TYPE_INVALID;
        }
    }
}
