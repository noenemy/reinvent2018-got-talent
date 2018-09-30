/*
  - 모든 테이블/컬럼명은 소문자로 표기
  - end_date를 제외한 나머지 날짜 컬럼은 default 값으로 현재 시간 기록
  - FK의 부모 키 삭제 시 FK 값도 삭제 / 변경 시 무시(...) -> 정해주세요
  - 랭킹 숫자값은 유저가 넘쳐나길 기원하는 마음으로 BIGINT로 설정
  - tb_game_rank_type 테이블의 'rank' 컬럼명은 예약어인 관계로 type_rank로 변경
  - URL 컬럼 타입은 스택오버플로의 중론에 따라 VARCHAR(2083)으로 지정 (MySQL 5.0 미만은 TEXT로, 상위 버전은 저걸로 하라고 되어 있음;;)
*/
use gottalent2018;

-- 게임 테이블. 유저 당 하나의 고유한 row가 생성되며, 플레이 완료 후 end_date가 업데이트되는 구조임
CREATE TABLE tb_game
(
  game_id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(100),
  share_yn CHAR(1) NOT NULL,
  start_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, 
  end_date TIMESTAMP,
  PRIMARY KEY(game_id)
) AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- 캐스팅 마스터 코드 테이블. 영화명, 배우(역할)명, 성별, 등급 정보
CREATE TABLE tb_cast
(
  cast_id INT NOT NULL AUTO_INCREMENT,
  title VARCHAR(200),
  actor VARCHAR(100),
  gender VARCHAR(10),
  grade VARCHAR(2),
  PRIMARY KEY(cast_id)
) AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- 게임 내 스테이지 별 로그 테이블
CREATE TABLE tb_stage_log
(
  game_id INT NOT NULL,
  action_type VARCHAR(100) NOT NULL,
  score DOUBLE,
  file_loc VARCHAR(2083),
  age TINYINT,
  gender VARCHAR(10),
  log_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY(game_id,action_type),
  FOREIGN KEY(game_id) 
    REFERENCES tb_game(game_id)
    ON DELETE CASCADE
    ON UPDATE RESTRICT
) DEFAULT CHARSET=utf8;

-- 게임 별 최종 결과 테이블. 전체 스코어, 랭킹, 캐스팅 결과 저장. 랭킹(total_rank)컬럼만 전체 사용자 플레이 결과에 따라 업데이트됨
CREATE TABLE tb_game_result
(
  game_id INT NOT NULL,
  result_page_url VARCHAR(2083),
  total_score DOUBLE,
  total_rank BIGINT,
  cast_result INT,
  grade_result VARCHAR(2),
  gender_result VARCHAR(10),
  age_result TINYINT,
  PRIMARY KEY(game_id),
  FOREIGN KEY(game_id) 
    REFERENCES tb_game(game_id)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  FOREIGN KEY(cast_result)
    REFERENCES tb_cast(cast_id)
    ON DELETE CASCADE
    ON UPDATE RESTRICT
) DEFAULT CHARSET=utf8;

-- 스테이지 타입 별 랭킹 테이블. action_type 별로 랭킹을 산정/결과 
CREATE TABLE tb_game_rank_type
(
  game_id INT NOT NULL,
  action_type VARCHAR(100) NOT NULL,
  type_rank BIGINT,
  PRIMARY KEY(game_id,action_type),
  FOREIGN KEY(game_id) 
    REFERENCES tb_game(game_id)
    ON DELETE CASCADE
    ON UPDATE RESTRICT
) DEFAULT CHARSET=utf8;

