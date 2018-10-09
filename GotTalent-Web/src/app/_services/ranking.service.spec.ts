/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RankingService } from './ranking.service';

describe('Service: Ranking', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RankingService]
    });
  });

  it('should ...', inject([RankingService], (service: RankingService) => {
    expect(service).toBeTruthy();
  }));
});
