using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using WebCommon;

/// <summary>
/// MetaData의 요약 설명입니다.
/// </summary>
public class MakeMetaData
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member variables

	public static PDMetaData CreateMetaData()
	{
		SqlConnection conn = null;

		try
		{
			conn = Util.OpenUserDBConnection();

			//
			//
			//

			DataRow drGameConfig = UserDB.GameConfig(conn, null);
			DataRowCollection drcCharacters = UserDB.Characters(conn, null);
			DataRowCollection drcContinents = UserDB.Continents(conn, null);
			DataRowCollection drcCharacterActions = UserDB.CharacterActions(conn, null);

			//
			//
			//

			PDMetaData metaData = new PDMetaData();
			
			//
			// 게임설정
			//
			if (drGameConfig != null)
			{
				PDGameConfig gameConfig = new PDGameConfig();
				gameConfig.Set(drGameConfig);

				metaData.gameConfig = gameConfig;
			}

			//
			// 캐릭터 목록
			//
			List<PDCharacter> characters = new List<PDCharacter>();
			foreach (DataRow dr in drcCharacters)
			{
				PDCharacter character = new PDCharacter();
				character.Set(dr);

				characters.Add(character);
			}
			metaData.characters = characters.ToArray();
			characters.Clear();

			//
			// 대륙 목록
			//
			List<PDContinent> continents = new List<PDContinent>();
			foreach (DataRow dr in drcContinents)
			{
				PDContinent continent = new PDContinent();
				continent.Set(dr);

				continents.Add(continent);
			}
			metaData.continents = continents.ToArray();
			continents.Clear();

			//
			// 캐릭터행동 목록
			//
			List<PDCharacterAction> characterActions = new List<PDCharacterAction>();
			foreach (DataRow dr in drcCharacterActions)
			{
				PDCharacterAction characterAction = new PDCharacterAction();
				characterAction.Set(dr);

				characterActions.Add(characterAction);
			}
			metaData.characterActions = characterActions.ToArray();
			characterActions.Clear();

			return metaData;
		}
		finally
		{
			if (conn != null)
				Util.Close(ref conn);
		}
	}
}