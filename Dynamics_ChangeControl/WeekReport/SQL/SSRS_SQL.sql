/*
- ����������(��) �� ��/�븦 ������ ��
- �ְ������� �ʵ��߰�
-> ����(������/�Ϸ�)
- SELECT �߰�
-> ���� ����(Ex. 2020�� 1�� 1����)
-> ����
-> %����
*/

-- ���� ������ (���� 1)
SELECT         WRD.CreatedByName AS '�����'
                       ,'1' AS '����'
                       ,WM1.new_name   AS '��з�'
                       ,WM2.new_name   AS '�ߺз�'
                       ,WM3.new_name   AS '�Һз�'
                       ,WRD.new_name AS '����'
                       , WRD.new_ntxt_description AS '����'
                       --,'������' AS '������Ȳ'
                       ,WRD.new_d_input_expected_monday         AS '��(����)'
                       ,WRD.new_d_input_real_monday                    AS '��(����)'
                       ,WRD.new_d_input_expected_tuesday        AS 'ȭ(����)'
                       ,WRD.new_d_input_real_tuesday                   AS 'ȭ(����)'
                       ,WRD.new_d_input_expected_wednesday              AS '��(����)'
                       ,WRD.new_d_input_real_wednesday                 AS '��(����)'
                       ,WRD.new_d_input_expected_thursday               AS '��(����)'
                       ,WRD.new_d_input_real_thursday                  AS '��(����)'
                       ,WRD.new_d_input_expected_friday         AS '��(����)'
                       ,WRD.new_d_input_real_friday                    AS '��(����)'
                       ,WRD.new_d_expected_sum                                AS '�հ�(����)'
                       ,WRD.new_d_real_sum                                           AS '�հ�(����)'
FROM           new_weekly_report_detail WRD
INNER JOIN      new_work_master1 WM1    ON WRD.new_l_work_master1 = WM1.new_work_master1Id
INNER JOIN      new_work_master2 WM2    ON WRD.new_l_work_master2 = WM2.new_work_master2Id 
INNER JOIN      new_work_master3 WM3    ON WRD.new_l_work_master3 = WM3.new_work_master3Id
WHERE   WRD.new_p_division = 100000000

UNION ALL

-- ������Ʈ (���� 0 ) 
SELECT         WRD.CreatedByName AS '�����'
                       , '0' AS '����'
                       ,CASE WHEN P.new_p_actiontype = 100000000 THEN '����' ELSE '����' END AS '��з�' -- 100000000 ���� 100000001 ����
                       ,PD.new_l_projectName AS '�ߺз�'
                       ,PD.new_name AS '�Һз�'
                       , WRD.new_name AS '����'
                       , WRD.new_ntxt_description AS '����'
                       --,CASE WHEN PD.new_p_statement = 100000004 THEN '�Ϸ�' ELSE '������' END AS  '������Ȳ' -- �Ϸ� ���� ���� ..
                       , WRD.new_d_input_expected_monday        AS '��(����)'
                       , WRD.new_d_input_real_monday                   AS '��(����)'
                       , WRD.new_d_input_expected_tuesday               AS 'ȭ(����)'
                       , WRD.new_d_input_real_tuesday                  AS 'ȭ(����)'
                       , WRD.new_d_input_expected_wednesday     AS '��(����)'
                       , WRD.new_d_input_real_wednesday         AS '��(����)'
                       , WRD.new_d_input_expected_thursday              AS '��(����)'
                       , WRD.new_d_input_real_thursday                 AS '��(����)'
                       , WRD.new_d_input_expected_friday        AS '��(����)'
                       , WRD.new_d_input_real_friday                   AS '��(����)'
                       , WRD.new_d_expected_sum                        AS '�հ�(����)'
                       , WRD.new_d_real_sum                                   AS '�հ�(����)'
FROM           new_weekly_report_detail WRD
INNER JOIN new_projectdetail PD ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN  new_project P ON  PD.new_l_project = P.new_projectId
WHERE   WRD.new_p_division = 100000001
