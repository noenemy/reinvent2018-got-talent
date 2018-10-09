export interface Game {
    game_id: number;
    name: string;
    share_yn: string;
    start_date: Date;
    end_date?: Date;
}
