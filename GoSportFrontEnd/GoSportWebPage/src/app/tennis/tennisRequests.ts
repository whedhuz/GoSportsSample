export interface ITennisEvent
{
  id: string;
  submitter: string;
  type: string;
  contentJson: string;
}

export interface ITennisEventObject
{
  gameId: string;
}