using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace moe.heing.CodeConfig {
    public class CodeConfig 
    {
        public const int TYPE_NEW_CLIENT = 0;
        public const int TYPE_MOVE_HOR = 1;
        public const int TYPE_JUMP = 2;
        public const int TYPE_UPLOAD_MY_MAP = 3;
        public const int TYPE_UPDATE_MAP = 4;
        public const int TYPE_CLIENT_EXIT = 5;

        public const int TYPE_NORMAL_UPLOAD = 1;
        public const int TYPE_SAVE_UPLOAD = 2;
        public const char OP_SAVE_GAME = 'a';
        public const char OP_NEW_LOGIN = 'b';

        public const int SERVER_PLAYER_ID = -1;
    }
}
