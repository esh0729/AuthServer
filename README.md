# AuthServer
.NET framework 기반의 인증 및 메타데이터 호출 서버

프로젝트 설명
- WebCommon : 클라이언트에 필요한 메타데이터 프로토콜
- WebSite : 요청 구현부

요청 설명
- Command.ashx를 호출하여 cmd QueryString에 요청할 내용을 읽어 각 요청들은 요청Handler를 호출하여 결과를 Json데이터로 전달
- SystemInfo : 시스템 관련 데이트 요청
- metadat : 인게임 메타데이터 요청
- login : 게임서버에 접속할 accessToken 요청, 접근시 id 필요
