// Copyright 2021, Infima Games. All Rights Reserved.

using Character;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Game Mode Service.
    /// </summary>
    public class GameModeService : IGameModeService
    {
        #region FIELDS
        
        /// <summary>
        /// The Player Character.
        /// </summary>
        private CharacterPlayer playerCharacterPlayer;
        
        #endregion
        
        #region FUNCTIONS
        
        public CharacterPlayer GetPlayerCharacter()
        {
            //Make sure we have a player character that is good to go!
            if (playerCharacterPlayer == null)
                playerCharacterPlayer = UnityEngine.Object.FindObjectOfType<CharacterPlayer>();
            
            //Return.
            return playerCharacterPlayer;
        }
        
        #endregion
    }
}