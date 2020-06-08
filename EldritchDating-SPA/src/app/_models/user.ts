import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    accountAge: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    location: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
