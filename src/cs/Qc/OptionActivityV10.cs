using System;

namespace JbnLib.Qc
{
    public class OptionActivityV10
    {
        private ActivityV10 _Activity;
        public ActivityV10 Activity
        {
            get { return _Activity; }
            set { _Activity = value; }
        }

        private float _ActivityWeight;
        public float ActivityWeight
        {
            get { return _ActivityWeight; }
            set { _ActivityWeight = value; }
        }

        public OptionActivityV10()
        {
        }
        public OptionActivityV10(ActivityV10 activity, float weight)
        {
            _Activity = activity;
            _ActivityWeight = weight;
        }

        public new string ToString()
        {
            return String.Format("{0} {1}", _Activity, _ActivityWeight);
        }
    }

    public enum ActivityV10 : int
    {
        ACT_INVALID = -1,
        ACT_RESET = 0,		    // Set m_Activity to this invalid value to force a reset to m_IdealActivity
        ACT_IDLE = 1,
        ACT_GUARD,
        ACT_WALK,
        ACT_RUN,
        ACT_FLY,				// Fly (and flap if appropriate)
        ACT_SWIM,
        ACT_HOP,				// vertical jump
        ACT_LEAP,				// long forward jump
        ACT_FALL,
        ACT_LAND,
        ACT_STRAFE_LEFT,
        ACT_STRAFE_RIGHT,
        ACT_ROLL_LEFT,			// tuck and roll, left
        ACT_ROLL_RIGHT,			// tuck and roll, right
        ACT_TURN_LEFT,			// turn quickly left (stationary)
        ACT_TURN_RIGHT,			// turn quickly right (stationary)
        ACT_CROUCH,				// the act of crouching down from a standing position
        ACT_CROUCHIDLE,			// holding body in crouched position (loops)
        ACT_STAND,				// the act of standing from a crouched position
        ACT_USE,
        ACT_SIGNAL1,
        ACT_SIGNAL2,
        ACT_SIGNAL3,
        ACT_TWITCH,
        ACT_COWER,
        ACT_SMALL_FLINCH,
        ACT_BIG_FLINCH,
        ACT_RANGE_ATTACK1,
        ACT_RANGE_ATTACK2,
        ACT_MELEE_ATTACK1,
        ACT_MELEE_ATTACK2,
        ACT_RELOAD,
        ACT_ARM,				// pull out gun, for instance
        ACT_DISARM,				// reholster gun
        ACT_EAT,				// monster chowing on a large food item (loop)
        ACT_DIESIMPLE,
        ACT_DIEBACKWARD,
        ACT_DIEFORWARD,
        ACT_DIEVIOLENT,
        ACT_BARNACLE_HIT,		// barnacle tongue hits a monster
        ACT_BARNACLE_PULL,		// barnacle is lifting the monster ( loop )
        ACT_BARNACLE_CHOMP,		// barnacle latches on to the monster
        ACT_BARNACLE_CHEW,		// barnacle is holding the monster in its mouth ( loop )
        ACT_SLEEP,
        ACT_INSPECT_FLOOR,		// for active idles, look at something on or near the floor
        ACT_INSPECT_WALL,		// for active idles, look at something directly ahead of you ( doesn't HAVE to be a wall or on a wall )
        ACT_IDLE_ANGRY,			// alternate idle animation in which the monster is clearly agitated. (loop)
        ACT_WALK_HURT,			// limp  (loop)
        ACT_RUN_HURT,			// limp  (loop)
        ACT_HOVER,				// Idle while in flight
        ACT_GLIDE,				// Fly (don't flap)
        ACT_FLY_LEFT,			// Turn left in flight
        ACT_FLY_RIGHT,			// Turn right in flight
        ACT_DETECT_SCENT,		// this means the monster smells a scent carried by the air
        ACT_SNIFF,				// this is the act of actually sniffing an item in front of the monster
        ACT_BITE,				// some large monsters can eat small things in one bite. This plays one time, EAT loops.
        ACT_THREAT_DISPLAY,		// without attacking, monster demonstrates that it is angry. (Yell, stick out chest, etc )
        ACT_FEAR_DISPLAY,		// monster just saw something that it is afraid of
        ACT_EXCITED,			// for some reason, monster is excited. Sees something he really likes to eat, or whatever.
        ACT_SPECIAL_ATTACK1,	// very monster specific special attacks.
        ACT_SPECIAL_ATTACK2,
        ACT_COMBAT_IDLE,		// agitated idle.
        ACT_WALK_SCARED,
        ACT_RUN_SCARED,
        ACT_VICTORY_DANCE,		// killed a player, do a victory dance.
        ACT_DIE_HEADSHOT,		// die, hit in head. 
        ACT_DIE_CHESTSHOT,		// die, hit in chest
        ACT_DIE_GUTSHOT,		// die, hit in gut
        ACT_DIE_BACKSHOT,		// die, hit in back
        ACT_FLINCH_HEAD,
        ACT_FLINCH_CHEST,
        ACT_FLINCH_STOMACH,
        ACT_FLINCH_LEFTARM,
        ACT_FLINCH_RIGHTARM,
        ACT_FLINCH_LEFTLEG,
        ACT_FLINCH_RIGHTLEG,
    }

}
