export interface TennisMatchDetails
{
  id: string;
  servingPlayer: number;
  setScores: TennisSetScore[];
  currentGameScore: TennisCurrentGameScore;
}

export interface TennisSetScore
{
  playerOneScore: number;
  playerTwoScore: number;
}

export interface TennisCurrentGameScore
{
  playerOneScore: string;
  playerTwoScore: string;
}