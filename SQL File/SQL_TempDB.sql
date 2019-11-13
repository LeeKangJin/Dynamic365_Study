/*
SELECT * FROM Incident I                     -- 서비스케이스
SELECT * FROM new_it_request R        -- IT요청서
SELECT * FROM new_request_type T      -- 요청서타입
SELECT * FROM new_approval A          -- 결재
*/

-- 요청자결재 리스트 세팅(임시테이블)
SELECT * INTO #APPROVAL1
FROM
(
        SELECT
               new_approvalId
               , new_l_it_request
               , new_p_approval_type
               , A.new_p_approval1, A.new_l_approver1Name, A.new_dt_approval1
               , A.new_p_approval2, A.new_l_approver2Name, A.new_dt_approval2
               , A.new_p_approval3, A.new_l_approver3Name, A.new_dt_approval3
               , A.new_p_approval4, A.new_l_approver4Name, A.new_dt_approval4
        FROM new_approval A
        WHERE new_p_approval_type = 100000000 --요청자결재
        AND new_l_it_request = 'D9F81BBB-709C-E911-80E5-00155D012E07' 
) T
SELECT * FROM #APPROVAL1

-- 내부결재 리스트 세팅(임시테이블)
SELECT * INTO #APPROVAL2
FROM
(
        SELECT
               new_approvalId
               , new_l_it_request
               , new_p_approval_type
               , A.new_p_approval1, A.new_l_approver1Name, A.new_dt_approval1
               , A.new_p_approval2, A.new_l_approver2Name, A.new_dt_approval2
               , A.new_p_approval3, A.new_l_approver3Name, A.new_dt_approval3
        FROM new_approval A
        WHERE new_p_approval_type = 100000001 --내부결재
        AND new_l_it_request = 'D9F81BBB-709C-E911-80E5-00155D012E07' 
) T
SELECT * FROM #APPROVAL2

-- #APPROVAL1, #APPROVAL2 조인하여 합침
SELECT * INTO #new_approval
FROM
(
        SELECT 
               A1.new_l_it_request

               /*요청자 결재 라인*/
               , A1.new_p_approval1 AS '요청자결재_결재자직책(1차)'
               , A1.new_l_approver1Name AS '요청자결재_결재자(1차)'
               , A1.new_dt_approval1 AS '요청자결재_결재일시(1차)'

               , A1.new_p_approval2 AS '요청자결재_결재자직책(2차)'
               , A1.new_l_approver2Name AS '요청자결재_결재자(2차)'
               , A1.new_dt_approval2 AS '요청자결재_결재일시(2차)'

               , A1.new_p_approval3 AS '요청자결재_결재자직책(3차)'
               , A1.new_l_approver3Name AS '요청자결재_결재자(3차)'
               , A1.new_dt_approval3 AS '요청자결재_결재일시(3차)'

               /*내부결재 라인*/
               , A2.new_p_approval1 AS '내부결재_결재자직책(1차)'
               , A2.new_l_approver1Name AS '내부결재_결재자(1차)'
               , A2.new_dt_approval1 AS '내부결재_결재일시(1차)'

               , A2.new_p_approval2 AS '내부결재_결재자직책(2차)'
               , A2.new_l_approver2Name AS '내부결재_결재자(2차)'
               , A2.new_dt_approval2 AS '내부결재_결재일시(2차)'

               , A2.new_p_approval3 AS '내부결재_결재자직책(3차)'
               , A2.new_l_approver3Name AS '내부결재_결재자(3차)'
               , A2.new_dt_approval3 AS '내부결재_결재일시(3차)'
        FROM #APPROVAL1 A1 
        INNER JOIN #APPROVAL2 A2 ON A1.new_l_it_request = A2.new_l_it_request
) T


SELECT * FROM #new_approval

SELECT
        /*서비스케이스 필드*/
        I.Title --서비스케이스 제목
        
        /*IT요청서 필드*/
        , R.new_name --요청서ID
        , R.new_txt_subject --요청제목
        , R.new_ntxt_purpose_detail --요청내용
        , R.new_dt_target_from
        , R.new_dt_target_to

        /*요청서타입 필드*/
        , T.new_p_request_item
        , T.new_txt_request_type_detail1
        , T.new_txt_request_type_detail2

        /*요청자 정보*/
        , C.AccountIdName
        , C.FullName
        , C.new_txt_companynum

        /*결재라인 정보*/
        , A.*
FROM Incident I
INNER JOIN new_it_request R ON I.new_l_it_request = R.new_it_requestId                     --IT요청서 
INNER JOIN new_request_type T ON R.new_l_request_type = T.new_request_typeId       --요청서타입
INNER JOIN Contact C ON I.CustomerId = C.ContactId                                                        --요청자
INNER JOIN #new_approval A ON R.new_it_requestId = A.new_l_it_request                      --결재
WHERE T.new_name = 'IT보안_네트워크_VPN 사용권한'



IF OBJECT_ID('tempdb..#APPROVAL1') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END

IF OBJECT_ID('tempdb..#APPROVAL2') IS NOT NULL
BEGIN DROP TABLE #APPROVAL2 END

IF OBJECT_ID('tempdb..#new_approval') IS NOT NULL
BEGIN DROP TABLE #new_approval END


