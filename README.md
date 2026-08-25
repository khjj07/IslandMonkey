# IslandMonkey

섬을 항해하며 시설을 배치하고 원숭이를 뽑기(가챠)로 수집/육성하는 모바일 캐주얼 수집형 게임입니다.

**개발 기간**: 2023.05 ~ 2023.10

## 사용 엔진

- Unity (C#, 모바일/안드로이드)

## 핵심 기술

- ScriptableObject 기반 데이터 주도(Data-Driven) 아키텍처 (구매/상점 UI 등)
- UniRx를 활용한 반응형 프로그래밍
- Firebase 연동 (백엔드/원격 설정)
- Google Play Games Services 연동
- 섬 시설 배치 시스템, 항해 씬 전환 구조
- 뽑기(가챠) 연출 및 튜토리얼 플로우

## 기여 개요

4인 팀 프로젝트로 참여하여 몬키 이동/배치 로직 리팩토링, 데이터테이블 연동, 구매 UI의 ScriptableObject화, 튜토리얼 플로우 및 뽑기 연출 등을 담당했습니다.

## 프로젝트 구조

```
Assets/0_IslandMonkey/
├─ Prefabs, Resources, Scenes, Test
└─ Scripts/
   ├─ Data/              # 데이터 정의
   ├─ ScriptableObject/   # SO 기반 데이터 자산
   ├─ Loader/             # 씬/데이터 로더
   ├─ Managers/           # 매니저 클래스
   ├─ Pattern/            # 공통 디자인 패턴
   ├─ Fundamentals/
   ├─ Extension/          # 확장 메서드
   ├─ UI/
   └─ Debug/
```
