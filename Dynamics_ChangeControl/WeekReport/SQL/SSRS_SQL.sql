/*
- ����� -> SSRS ó��? 

-> ����(������/�Ϸ�) : ���� �Է½� �Ϸ� ġ�� �ڵ� �ɼ����� �߰� 

- SELECT �߰�

- �� �߰� 

-> %���� -> �븮�� ���� �ʿ� 

*/



-- ���� ������ (���� 1)
SELECT					WRD.CreatedByName AS '�����'
                       ,'���� Master' AS '����'
					   ,WRD.new_p_check AS '�ϷῩ��'
                       ,WM3.new_l_work_master1Name   AS '��з�'
                       ,WM3.new_l_work_master2Name   AS '�ߺз�'
                       ,WM3.new_name   AS '�Һз�'
                       ,WRD.new_name AS '����'
                       , WRD.new_ntxt_description AS '����'
                       ,WRD.new_d_input_expected_monday										AS '��(����)'
                       ,WRD.new_d_input_real_monday											AS '��(����)'
					   ,WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday		AS '��(����)'		
                       ,WRD.new_d_input_expected_tuesday									AS 'ȭ(����)'
                       ,WRD.new_d_input_real_tuesday										AS 'ȭ(����)'
					   ,WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday     AS 'ȭ(����)'
                       ,WRD.new_d_input_expected_wednesday									AS '��(����)'
                       ,WRD.new_d_input_real_wednesday										AS '��(����)'
                       ,WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday AS '��(����)' 
					   ,WRD.new_d_input_expected_thursday									AS '��(����)'
                       ,WRD.new_d_input_real_thursday										AS '��(����)'
                       ,WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday   AS '��(����)' 
					   ,WRD.new_d_input_expected_friday										AS '��(����)'
                       ,WRD.new_d_input_real_friday											AS '��(����)'
                       ,WRD.new_d_input_expected_friday - WRD.new_d_input_real_friday       AS '��(����)' 
					   ,WRD.new_d_expected_sum												AS '�հ�(����)'
                       ,WRD.new_d_real_sum													AS '�հ�(����)'
					   ,WRD.new_d_expected_sum - WRD.new_d_real_sum                         AS '�հ�(����)'
					   ,WR.new_name														    AS '����'
FROM            new_weekly_report_detail WRD
INNER JOIN      new_work_master3  WM3    ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN      new_weekly_report WR     ON WR.new_weekly_reportId = WRD.new_l_weekly_report
WHERE   WRD.new_p_division = 100000000


UNION ALL

-- ������Ʈ (���� 0 ) 
SELECT         WRD.CreatedByName AS '�����'
                       , '������Ʈ' AS '����'
                       ,CASE WHEN P.new_p_actiontype = 100000000 THEN '����' ELSE '����' END AS '��з�' -- 100000000 ���� 100000001 ����
                       ,PD.new_l_projectName AS '�ߺз�'
                       ,PD.new_name AS '�Һз�'
                       , WRD.new_name AS '����'
                       , WRD.new_ntxt_description AS '����'
                       , WRD.new_d_input_expected_monday										 AS '��(����)'
                       , WRD.new_d_input_real_monday											 AS '��(����)'
					   , WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday	         AS '��(����)'
                       , WRD.new_d_input_expected_tuesday										 AS 'ȭ(����)'
                       , WRD.new_d_input_real_tuesday											 AS 'ȭ(����)'
					   , WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday         AS 'ȭ(����)'											 
                       , WRD.new_d_input_expected_wednesday										 AS '��(����)'
                       , WRD.new_d_input_real_wednesday											 AS '��(����)'
                       , WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday     AS '��(����)'
					   , WRD.new_d_input_expected_thursday										 AS '��(����)'
                       , WRD.new_d_input_real_thursday											 AS '��(����)'
                       , WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday       AS '��(����)'
					   , WRD.new_d_input_expected_friday										 AS '��(����)'
                       , WRD.new_d_input_real_friday											 AS '��(����)'
                       , WRD.new_d_input_expected_friday -  WRD.new_d_input_real_friday          AS '��(����)'
					   , WRD.new_d_expected_sum													 AS '�հ�(����)'
                       , WRD.new_d_real_sum													     AS '�հ�(����)'
					   , WRD.new_d_expected_sum - WRD.new_d_real_sum                             AS '�հ�(����)'
					   , WR.new_name															 AS '����'
FROM           new_weekly_report_detail WRD
INNER JOIN new_projectdetail PD ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN new_project P ON  PD.new_l_project = P.new_projectId
INNER JOIN new_weekly_report WR     ON WR.new_weekly_reportId = WRD.new_l_weekly_report

WHERE   WRD.new_p_division = 100000001
